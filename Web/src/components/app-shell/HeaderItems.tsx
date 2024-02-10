import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";

function HeaderItems() {
  return (
    <Group justify="space-between">
      <Group gap={0}>
        <UnstyledButton className="control">Currency</UnstyledButton>
        <UnstyledButton className="control">Temperature</UnstyledButton>
        <UnstyledButton className="control">Number.Base</UnstyledButton>
      </Group>

      <Select
        placeholder="Search for a category..."
        data={["Currency", "Temperature", "Number.Base"]}
        maxDropdownHeight={200}
        searchable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

export default HeaderItems;
