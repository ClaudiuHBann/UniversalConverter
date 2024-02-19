import { NavigateFunction, createSearchParams } from "react-router-dom";
import { UCContext } from "../contexts/UCContext";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";
import { ESearchParam } from "./Enums";
import { ToSearchParam } from "./EnumsExtensions";

export function NavigateTo(
  navigate: NavigateFunction,
  context: UCContext,
  searchParams: URLSearchParams
) {
  const searchParamToValue = ParseSearchParams(searchParams);
  NavigateToEx(navigate, context, searchParamToValue);
}

function ParseSearchParams(searchParams: URLSearchParams) {
  const searchParamToValue = new Map<ESearchParam, string | null>();
  searchParams.forEach((value, key) => {
    const searchParam = ToSearchParam(key);
    if (!searchParam) {
      return;
    }
    searchParamToValue.set(searchParam, value);
  });

  return searchParamToValue;
}

function NavigateToEx(
  navigate: NavigateFunction,
  context: UCContext,
  searchParamToValue: Map<ESearchParam, string | null>
) {
  const searchParams = createSearchParams();
  if (!searchParamToValue.has(ESearchParam.Category)) {
    return searchParams;
  }

  const category = searchParamToValue.get(ESearchParam.Category)!;
  searchParamToValue.forEach((value, searchParam) =>
    AddSearchParam(context, searchParams, category, searchParam, value)
  );

  navigate({
    pathname: "/",
    search: searchParams.toString(),
  });
}

function AddSearchParam(
  context: UCContext,
  searchParams: URLSearchParams,
  category: string | null,
  searchParam: ESearchParam,
  value: string | null
) {
  if (!category || !context.HasCategory(category)) {
    return;
  }

  searchParams.set(ESearchParam.Category, ToLowerCaseAndCapitalize(category));
  if (!value || !context.HasFromTo(category, value)) {
    return;
  }

  value = context.FindFromTo(category, value);
  switch (searchParam) {
    case ESearchParam.From:
      searchParams.set(ESearchParam.From, value!);
      break;
    case ESearchParam.To:
      searchParams.set(ESearchParam.To, value!);
      break;
  }
}

export function NavigateToCategory(
  navigate: NavigateFunction,
  context: UCContext,
  category: string | null
) {
  NavigateToEx(navigate, context, new Map([[ESearchParam.Category, category]]));
}

export function NavigateToRoot(navigate: NavigateFunction) {
  navigate("/");
}
