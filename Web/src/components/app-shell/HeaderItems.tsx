import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";
import { useContext } from "react";
import { UCContext } from "../../contexts/UCContext";
import {
  NavigateFunction,
  createSearchParams,
  useNavigate,
} from "react-router-dom";

function NavigateToCategory(
  navigate: NavigateFunction,
  category: string | null
) {
  if (!category) {
    return;
  }

  navigate({
    pathname: "/",
    search: createSearchParams({
      category: category,
    }).toString(),
  });
}

function HeaderItems() {
  const context = useContext(UCContext);
  const navigate = useNavigate();

  return (
    <Group justify="space-between">
      <Group gap={0}>
        {context.categories.map((category, index) => {
          return (
            <UnstyledButton
              key={index}
              className="control"
              onClick={() => NavigateToCategory(navigate, category)}
            >
              {category}
            </UnstyledButton>
          );
        })}
      </Group>

      <Select
        placeholder="Search for a category..."
        data={context.categories}
        onChange={(value) => NavigateToCategory(navigate, value)}
        maxDropdownHeight={200}
        searchable
        clearable
        nothingFoundMessage="Nothing found..."
      />
    </Group>
  );
}

export default HeaderItems;
