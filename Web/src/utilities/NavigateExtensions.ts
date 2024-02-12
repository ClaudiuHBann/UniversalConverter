import { NavigateFunction, createSearchParams } from "react-router-dom";
import { UCContext } from "../contexts/UCContext";
import { ToLowerCaseAndCapitalize } from "./StringExtensions";
import { SearchParam } from "./Enums";
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
  const searchParamToValue = new Map<SearchParam, string | null>();
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
  searchParamToValue: Map<SearchParam, string | null>
) {
  const searchParams = createSearchParams();
  if (!searchParamToValue.has(SearchParam.Category)) {
    return searchParams;
  }

  const category = searchParamToValue.get(SearchParam.Category)!;
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
  searchParam: SearchParam,
  value: string | null
) {
  if (!category || !context.hasCategory(category)) {
    return;
  }

  searchParams.set(SearchParam.Category, ToLowerCaseAndCapitalize(category));
  if (!value || !context.hasFromTo(category, value)) {
    return;
  }

  value = ToLowerCaseAndCapitalize(value);
  switch (searchParam) {
    case SearchParam.From:
      searchParams.set(SearchParam.From, value);
      break;
    case SearchParam.To:
      searchParams.set(SearchParam.To, value);
      break;
  }
}

export function NavigateToCategory(
  navigate: NavigateFunction,
  context: UCContext,
  category: string | null
) {
  NavigateToEx(navigate, context, new Map([[SearchParam.Category, category]]));
}

export function NavigateToRoot(navigate: NavigateFunction) {
  navigate("/");
}
