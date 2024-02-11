import { Stack, Text } from "@mantine/core";
import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import AppShellEx from "./app-shell/AppShellEx";
import { useLocation } from "react-router-dom";
import { UCContext } from "../contexts/UCContext.tsx";
import { useContext } from "react";
import { SearchParam } from "../utilities/Enums.ts";

function FindCategoryHeader(
  context: UCContext | undefined,
  category: string | null
) {
  var text = "Choose a category...";
  if (!context || !category) {
    return text;
  }

  if (context.hasCategory(category)) {
    text = `${category} Converter`;
  }

  return text;
}

function App() {
  const context = useContext(UCContext);

  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);

  const category = searchParams.get(SearchParam.Category);

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

export default App;
