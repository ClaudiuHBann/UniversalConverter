import { Stack, Text } from "@mantine/core";
import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import AppShellEx from "./app-shell/AppShellEx";
import { useLocation } from "react-router-dom";
import { ESearchParam } from "../utilities/Enums.ts";
import { ToLowerCaseAndCapitalize } from "../utilities/StringExtensions.ts";
import { UCContext, useUCContext } from "../contexts/UCContext.ts";

function FindCategoryHeader(context: UCContext, category: string | null) {
  var text = "Choose a category...";
  if (!context || !category) {
    return text;
  }

  if (context.HasCategory(category)) {
    text = `${ToLowerCaseAndCapitalize(category)} Converter`;
  }

  return text;
}

function Main() {
  const context = useUCContext();

  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);

  const category = searchParams.get(ESearchParam.Category);

  return (
    <AppShellEx>
      <Stack>
        <Text>{FindCategoryHeader(context, category)}</Text>

        <Input />
        <Actions />
        <Output />
      </Stack>
    </AppShellEx>
  );
}

export default Main;
