import { Center, Stack, Text, rem } from "@mantine/core";
import Input from "./io/Input.tsx";
import Output from "./io/Output.tsx";
import Actions from "./Actions.tsx";
import { useLocation } from "react-router-dom";
import { ToLowerCaseAndCapitalize } from "../../utilities/StringExtensions.ts";
import { UCContext, useUCContext } from "../../contexts/UCContext.ts";
import AppShellEx from "./app-shell/AppShellEx.tsx";
import { URLSearchParamsEx } from "../../utilities/URLSearchParamsEx.ts";

function FindCategoryHeader(
  context: UCContext | null,
  category: string | null
) {
  let text = "Choose a category...";
  if (!context || !category) {
    return text;
  }

  if (context.HasCategory(category)) {
    text = `${ToLowerCaseAndCapitalize(category)} Converter`;
  }

  return text;
}

function Root() {
  const context = useUCContext();
  const location = useLocation();

  const searchParams = new URLSearchParamsEx(context, location.search);
  const category = searchParams.GetCategory();

  return (
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
  );
}

export default Root;
