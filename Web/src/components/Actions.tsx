import { ActionIcon, Flex, Select } from "@mantine/core";
import { IconCircleArrowDown, IconSwitchHorizontal } from "@tabler/icons-react";
import { useDisclosure } from "@mantine/hooks";

function Actions() {
  const [loading, { toggle }] = useDisclosure();

  return (
    <Flex mih={50} gap="md" justify="center" align="center">
      <Select
        placeholder="Select from"
        data={["a", "b", "c"]}
        maxDropdownHeight={200}
        searchable
        nothingFoundMessage="Nothing found..."
      />

      <ActionIcon loading={loading} variant="subtle" onClick={toggle}>
        <IconCircleArrowDown color="gray" />
      </ActionIcon>

      <ActionIcon variant="subtle">
        <IconSwitchHorizontal color="gray" />
      </ActionIcon>

      <Select
        placeholder="Select to"
        data={["a", "b", "c"]}
        maxDropdownHeight={200}
        searchable
        nothingFoundMessage="Nothing found..."
      />
    </Flex>
  );
}

export default Actions;
