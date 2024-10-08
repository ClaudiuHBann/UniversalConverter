import { Group, UnstyledButton, Select } from "@mantine/core";
import "./HeaderItems.css";
import { useUCContext } from "../../../contexts/UCContext";
import { useNavigate } from "react-router-dom";
import { NavigateToCategory } from "../../../utilities/NavigateExtensions";
import { ToCategory } from "../../../utilities/EnumsExtensions";
import { useRankConverters } from "../../../hooks/Queries";
import { RankRequest } from "../../../models/requests/RankRequest";
import { useEffect, useState } from "react";

export interface HeaderItemsProps {
  openedNavbar: boolean;
  toggleNavbar: () => void;
  props?: React.ComponentProps<typeof Group>;
}

function HeaderItems({ openedNavbar, toggleNavbar, props }: HeaderItemsProps) {
  const context = useUCContext();
  const navigate = useNavigate();

  const categoriesCount = 3;

  const queryRankConverters = useRankConverters(
    context!,
    new RankRequest(categoriesCount)
  );
  const [categories, setCategories] = useState<string[]>([]);

  useEffect(() => {
    if (queryRankConverters.data) {
      setCategories(queryRankConverters.data.converters);
    } else if (context) {
      setCategories(context.GetCategories().slice(0, categoriesCount));
    }
  }, [queryRankConverters.data, context]);

  const HandleCategoryChange = (category: string | null) => {
    const eCategory = ToCategory(category);
    if (!eCategory || !context) {
      return;
    }

    NavigateToCategory(navigate, context, eCategory);

    if (openedNavbar) {
      toggleNavbar();
    }
  };

  return (
    <Group justify="space-between" {...props}>
      <Group gap={0}>
        {categories.map((category, index) => {
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
