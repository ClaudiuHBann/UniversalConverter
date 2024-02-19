import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";
import { useUCContext } from "../../contexts/UCContext";
import { useNavigate } from "react-router-dom";
import { NavigateToCategory } from "../../utilities/NavigateExtensions";

function HeaderItems() {
  const context = useUCContext();
  const navigate = useNavigate();

  return (
    <Group justify="space-between">
      <Group gap={0}>
        {context.GetCategories().map((category, index) => {
          return (
            <UnstyledButton
              key={index}
              className="control"
              onClick={() => NavigateToCategory(navigate, context, category)}
            >
              {category}
            </UnstyledButton>
          );
        })}
      </Group>

      <Select
        placeholder="Search for a category..."
        data={context.GetCategories()}
        onChange={(value) => NavigateToCategory(navigate, context, value)}
        maxDropdownHeight={200}
        searchable
        clearable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

export default HeaderItems;
