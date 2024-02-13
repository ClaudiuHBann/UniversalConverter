import { ActionIcon, Flex, Select, Tooltip, rem } from "@mantine/core";
import {
  IconCheck,
  IconCircleArrowDown,
  IconSwitchHorizontal,
} from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";
import { useContext, useState } from "react";
import { UCContext } from "../contexts/UCContext";
import { useNavigate, useSearchParams } from "react-router-dom";
import { SearchParam } from "../utilities/Enums";
import { NavigateTo } from "../utilities/NavigateExtensions";
import ActionIconEx from "./ActionIconEx";

function FindTooltipConvert(state: boolean) {
  return state ? "Converting..." : "Convert";
}

function Convert(toggle: () => void) {
  toggle();
  // TODO: Convert
  toggle();
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
  const [loading, { toggle }] = useDisclosure();
  const [searchParams] = useSearchParams();
  const context = useContext(UCContext);
  const navigate = useNavigate();

  const category = searchParams.get(SearchParam.Category);

  const [fromValue, setFromValue] = useState<string | null>(
    context.findFromTo(category, searchParams.get(SearchParam.From))
  );
  const [toValue, setToValue] = useState<string | null>(
    context.findFromTo(category, searchParams.get(SearchParam.To))
  );

  const SwapFromTo = () => {
    setFromValue(toValue);
    if (toValue) {
      searchParams.set(SearchParam.From, toValue);
    }

    setToValue(fromValue);
    if (fromValue) {
      searchParams.set(SearchParam.To, fromValue);
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
      searchParams.set(SearchParam.From, value);
    } else {
      setToValue(value);
      searchParams.set(SearchParam.To, value);
    }

    NavigateTo(navigate, context, searchParams);
  };

  return (
    <Flex mih={50} gap="md" justify="center" align="center">
      <Select
        placeholder="Select from"
        data={context.fromTo(category)}
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
          onClick={() => Convert(toggle)}
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
        data={context.fromTo(category)}
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
