import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";
import { useUCContext } from "../../../contexts/UCContext";
import { useNavigate } from "react-router-dom";
import { NavigateToCategory } from "../../../utilities/NavigateExtensions";
import { ToCategory } from "../../../utilities/EnumsExtensions";

function HeaderItems() {
  const context = useUCContext();
  const navigate = useNavigate();

  const HandleCategoryChange = (category: string | null) => {
    var eCategory = ToCategory(category);
    if (!eCategory || !context) {
      return;
    }

    NavigateToCategory(navigate, context, eCategory);
  };

  return (
    <Group justify="space-between">
      <Group gap={0}>
        {context &&
          context.GetCategories().map((category, index) => {
            return (
              <UnstyledButton
                key={index}
                className="control"
                onClick={() => HandleCategoryChange(category)}
              >
                {category}
              </UnstyledButton>
            );
          })}
      </Group>

      <Select
        placeholder="Search for a category..."
        data={context ? context.GetCategories() : []}
        onChange={(category) => HandleCategoryChange(category)}
        maxDropdownHeight={200}
        searchable
        clearable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

export default HeaderItems;
