import { ActionIcon, Flex, Select, Tooltip, rem } from "@mantine/core";
import {
  IconCheck,
  IconCircleArrowDown,
  IconSwitchHorizontal,
} from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";
import { useUCContext } from "../contexts/UCContext";
import { useNavigate, useSearchParams } from "react-router-dom";
import { ESearchParam } from "../utilities/Enums";
import { NavigateTo } from "../utilities/NavigateExtensions";
import ActionIconEx from "./extensions/ActionIconEx";
import { useEffect, useState } from "react";
import { useConvert } from "../hooks/Queries";
import { ToCategory } from "../utilities/EnumsExtensions";
import {
  CreateRequest,
  ParseInput,
  ToRequest,
} from "../utilities/RequestExtensions";
import { ToOutput } from "../utilities/ResponseExtensions";

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

function Actions() {
  const [loading, toggle] = useDisclosure(false);
  const [searchParams] = useSearchParams();
  const context = useUCContext();
  const navigate = useNavigate();

  const category = searchParams.get(ESearchParam.Category);

  const fromValueNew = context.FindFromTo(
    category,
    searchParams.get(ESearchParam.From)
  );
  const [fromValue, setFromValue] = useState<string | null>(fromValueNew);
  useEffect(() => setFromValue(fromValueNew), [fromValueNew]);

  const toValueNew = context.FindFromTo(
    category,
    searchParams.get(ESearchParam.To)
  );
  const [toValue, setToValue] = useState<string | null>(toValueNew);
  useEffect(() => setToValue(toValueNew), [toValueNew]);

  const convertHook = useConvert(ToCategory(category)!);
  const convert = async () => {
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
      const [_, setOutputValue] = context.GetOutput();
      setOutputValue(ToOutput(response));
    }

    toggle.close();
  };

  const SwapFromTo = () => {
    setFromValue(toValue);
    if (toValue) {
      searchParams.set(ESearchParam.From, toValue);
    }

    setToValue(fromValue);
    if (fromValue) {
      searchParams.set(ESearchParam.To, fromValue);
    }

    NavigateTo(navigate, context, searchParams);
  };

  const OnChangeFromTo = (value: string | null, fromTo: boolean) => {
    if (fromValue === value || toValue === value) {
      SwapFromTo();
      return;
    }

    if (!value) {
      value = "";
    }

    if (fromTo) {
      setFromValue(value);
      searchParams.set(ESearchParam.From, value);
    } else {
      setToValue(value);
      searchParams.set(ESearchParam.To, value);
    }

    NavigateTo(navigate, context, searchParams);
  };

  return (
    <Flex mih={50} gap="md" justify="center" align="center">
      <Select
        placeholder="Select from"
        data={context.GetFromTo(category)}
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
        data={context.GetFromTo(category)}
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
