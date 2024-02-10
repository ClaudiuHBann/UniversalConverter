import { Stack, Text } from "@mantine/core";
import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import AppShellEx from "./app-shell/AppShellEx";
import { useLocation } from "react-router-dom";
import { UCContext } from "../contexts/UCContext.tsx";
import { useContext } from "react";

function FindCategoryHeader(context: UCContext, category: string | null) {
  var text = "Choose a category...";
  if (!category) {
    return text;
  }

  category = category.toLowerCase();
  category = category[0].toUpperCase() + category.slice(1);

  if (context.categories.includes(category)) {
    text = `${category} Converter`;
  }

  return text;
}

function App() {
  const context = useContext(UCContext);

  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);

  const category = searchParams.get("category");

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
