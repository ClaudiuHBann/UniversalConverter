import { Center, Stack, Text, rem } from "@mantine/core";
import Input from "./io/Input";
import Output from "./io/Output";
import Actions from "./Actions";
import AppShellEx from "./app-shell/AppShellEx";
import { useLocation } from "react-router-dom";
import { ESearchParam } from "../../utilities/Enums.ts";
import { ToLowerCaseAndCapitalize } from "../../utilities/StringExtensions.ts";
import { UCContext, useUCContext } from "../../contexts/UCContext.ts";
import UCContextProvider from "../../components/UCContextProvider.tsx";

function FindCategoryHeader(
  context: UCContext | null,
  category: string | null
) {
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
    <UCContextProvider>
      <AppShellEx>
        <Center>
          <Stack w="66%" mt={rem(18)} gap={rem(22)}>
            <Text size={rem(31)}>{FindCategoryHeader(context, category)}</Text>

            <Input />
            <Actions />
            <Output />
          </Stack>
        </Center>
      </AppShellEx>
    </UCContextProvider>
  );
}

export default Main;
