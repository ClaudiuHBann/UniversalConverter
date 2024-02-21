import { NavigateFunction, createSearchParams } from "react-router-dom";
import { UCContext } from "../contexts/UCContext";
import { ECategory, ESearchParam } from "./Enums";
import { ToCategory, ToSearchParam } from "./EnumsExtensions";

export function NavigateTo(
  navigate: NavigateFunction,
  context: UCContext,
  searchParams: URLSearchParams
) {
  const searchParamToValue = ParseSearchParams(searchParams);
  NavigateToEx(navigate, context, searchParamToValue);
}

function ParseSearchParams(searchParams: URLSearchParams) {
  const searchParamToValue = new Map<ESearchParam, string>();
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
  searchParamToValue: Map<ESearchParam, string>
) {
  const searchParams = createSearchParams();
  if (!searchParamToValue.has(ESearchParam.Category)) {
    return searchParams;
  }

  const category = searchParamToValue.get(ESearchParam.Category)!;
  searchParamToValue.forEach((value, searchParam) => {
    const eCategory = ToCategory(category);
    if (eCategory) {
      AddSearchParam(context, searchParams, eCategory, searchParam, value);
    }
  });

  navigate({
    pathname: "/",
    search: searchParams.toString(),
  });
}

function AddSearchParam(
  context: UCContext,
  searchParams: URLSearchParams,
  category: ECategory,
  searchParam: ESearchParam,
  value: string
) {
  if (!category || !context.HasCategory(category)) {
    return;
  }

  searchParams.set(ESearchParam.Category, context.FindCategory(category)!);
  if (!value || !context.HasFromTo(category, value)) {
    return;
  }

  const fromTo = context.FindFromTo(category, value);
  if (!fromTo) {
    return;
  }

  switch (searchParam) {
    case ESearchParam.From:
      searchParams.set(ESearchParam.From, fromTo);
      break;
    case ESearchParam.To:
      searchParams.set(ESearchParam.To, fromTo);
      break;
  }
}

export function NavigateToCategory(
  navigate: NavigateFunction,
  context: UCContext,
  category: ECategory
) {
  NavigateToEx(navigate, context, new Map([[ESearchParam.Category, category]]));
}

export function NavigateToRoot(navigate: NavigateFunction) {
  navigate("/");
}
