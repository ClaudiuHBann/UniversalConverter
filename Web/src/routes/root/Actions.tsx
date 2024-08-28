import { ActionIcon, Flex, Select, Tooltip, rem } from "@mantine/core";
import {
  IconCheck,
  IconCircleArrowDown,
  IconSwitchHorizontal,
} from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";
import { UCContext, useUCContext } from "../../contexts/UCContext";
import { useNavigate } from "react-router-dom";
import { NavigateTo } from "../../utilities/NavigateExtensions";
import ActionIconEx from "../../components/ActionIconEx";
import { useEffect, useState } from "react";
import { useConvert } from "../../hooks/Queries";
import { ToCategory } from "../../utilities/EnumsExtensions";
import {
  CreateRequest,
  ParseInput,
  ToRequest,
} from "../../models/requests/RequestExtensions";
import { ToOutput } from "../../models/responses/ResponseExtensions";
import { URLSearchParamsEx } from "../../utilities/URLSearchParamsEx";

function FindTooltipConvert(state: boolean) {
  return state ? "Converting..." : "Convert";
}

function FindIconSwap(state: boolean) {
  if (state) {
    return <IconCheck style={{ width: rem(69) }} />;
  } else {
    return <IconSwitchHorizontal style={{ width: rem(69) }} />;
  }
}

function FindTooltipSwap(state: boolean) {
  return state ? "Swapped" : "Swap";
}

function FindFromTo(
  context: UCContext | null,
  category: string | null,
  searchParamsEx: URLSearchParamsEx,
  fromTo: boolean
): string | null {
  if (!context) {
    return null;
  }

  const fromToParam = searchParamsEx.GetFromOrTo(fromTo);

  return (
    context.FindFromTo(category, fromToParam) ||
    context.GetFromToDefault(category, fromTo)
  );
}

function Actions() {
  const [loading, toggle] = useDisclosure(false);
  const context = useUCContext();
  const searchParamsEx = new URLSearchParamsEx(context);
  const navigate = useNavigate();

  const category = searchParamsEx.GetCategory();

  const fromValueNew = FindFromTo(context, category, searchParamsEx, true);
  const [fromValue, setFromValue] = useState<string | null>(fromValueNew);

  const toValueNew = FindFromTo(context, category, searchParamsEx, false);
  const [toValue, setToValue] = useState<string | null>(toValueNew);

  const UpdateFrom = (value: string | null) => {
    setFromValue(value);

    if (value) {
      searchParamsEx.SetFrom(value);
    }
  };

  const UpdateTo = (value: string | null) => {
    setToValue(value);

    if (value) {
      searchParamsEx.SetTo(value);
    }
  };

  useEffect(() => {
    if (!context || !fromValueNew || !toValueNew) {
      return;
    }

    UpdateFrom(fromValueNew);
    UpdateTo(toValueNew);

    NavigateTo(navigate, context, searchParamsEx);
  }, [fromValueNew, toValueNew]);

  const convertHook = useConvert(ToCategory(category)!, context!);
  const convert = async () => {
    if (!context) {
      return;
    }

    const eCategory = ToCategory(category);
    if (loading || !eCategory || !fromValue || !toValue) {
      return;
    }

    const eRequest = ToRequest(eCategory);

    const [inputValue] = context.GetInput();
    const inputParsed = ParseInput(eRequest, inputValue);
    const request = CreateRequest(eRequest, fromValue, toValue, inputParsed);

    toggle.open();

    const response = await convertHook.mutateAsync(request);
    if (response) {
      const [, setOutputValue] = context.GetOutput();
      setOutputValue(ToOutput(response));
    }

    toggle.close();
  };

  const SwapFromTo = () => {
    if (!context) {
      return;
    }

    UpdateTo(fromValue);
    UpdateFrom(toValue);

    NavigateTo(navigate, context, searchParamsEx);
  };

  const OnChangeFromTo = (value: string | null, fromTo: boolean) => {
    if (!context) {
      return;
    }

    if (fromValue === value || toValue === value) {
      SwapFromTo();
      return;
    }

    if (fromTo) {
      UpdateFrom(value);
    } else {
      UpdateTo(value);
    }

    NavigateTo(navigate, context, searchParamsEx);
  };

  return (
    <Flex mih={50} gap="md" justify="center" align="center">
      <Select
        placeholder="Select from"
        data={context ? context.GetFromTo(category) : []}
        maxDropdownHeight={200}
        value={fromValue}
        onChange={(value) => OnChangeFromTo(value, true)}
        searchable
        nothingFoundMessage="Nothing found..."
      />

      <Tooltip label={FindTooltipConvert(loading)} color="gray">
        <ActionIcon
          loading={loading}
          variant="subtle"
          onClick={async () => await convert()}
        >
          <IconCircleArrowDown color="gray" />
        </ActionIcon>
      </Tooltip>

      <ActionIconEx
        onClick={SwapFromTo}
        findIcon={FindIconSwap}
        findTooltip={FindTooltipSwap}
      />

      <Select
        placeholder="Select to"
        data={context ? context.GetFromTo(category) : []}
        maxDropdownHeight={200}
        value={toValue}
        onChange={(value) => OnChangeFromTo(value, false)}
        searchable
        nothingFoundMessage="Nothing found..."
      />
    </Flex>
  );
}

export default Actions;
