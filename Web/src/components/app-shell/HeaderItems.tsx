import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";
import { useContext } from "react";
import { UCContext } from "../../contexts/UCContext";

function HeaderItems() {
  const context = useContext(UCContext);

  return (
    <Group justify="space-between">
      <Group gap={0}>
        <UnstyledButton className="control">Currency</UnstyledButton>
        <UnstyledButton className="control">Temperature</UnstyledButton>
        <UnstyledButton className="control">Radix</UnstyledButton>
      </Group>

      <Select
        placeholder="Search for a category..."
        data={context.categories}
        maxDropdownHeight={200}
        searchable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

export default HeaderItems;
