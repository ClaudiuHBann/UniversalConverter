import { Center, Stack, Text, rem } from "@mantine/core";
import Input from "./io/Input.tsx";
import Output from "./io/Output.tsx";
import Actions from "./Actions.tsx";
import { useLocation } from "react-router-dom";
import { ESearchParam } from "../../utilities/Enums.ts";
import { ToLowerCaseAndCapitalize } from "../../utilities/StringExtensions.ts";
import { UCContext, useUCContext } from "../../contexts/UCContext.ts";
import AppShellEx from "./app-shell/AppShellEx.tsx";

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

function Root() {
  const context = useUCContext();

  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);

  const category = searchParams.get(ESearchParam.Category);

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
