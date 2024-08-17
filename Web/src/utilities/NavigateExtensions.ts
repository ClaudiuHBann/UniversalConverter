import { NavigateFunction } from "react-router-dom";
import { UCContext } from "../contexts/UCContext";
import { ECategory, ESearchParam } from "./Enums";
import { ToCategory } from "./EnumsExtensions";
import { URLSearchParamsEx } from "./URLSearchParamsEx";

export function NavigateTo(
  navigate: NavigateFunction,
  context: UCContext,
  searchParams: URLSearchParamsEx
) {
  NavigateToEx(navigate, context, searchParams.Map());
}

function NavigateToEx(
  navigate: NavigateFunction,
  context: UCContext,
  searchParamToValue: Map<ESearchParam, string>
) {
  const searchParams = new URLSearchParamsEx(context);
  if (!searchParamToValue.has(ESearchParam.Category)) {
    return;
  }

  const category = searchParamToValue.get(ESearchParam.Category)!;
  searchParamToValue.forEach((value, searchParam) => {
    const eCategory = ToCategory(category);
    if (eCategory) {
      searchParams.AddCategoryAndFromOrTo(eCategory, searchParam, value);
    }
  });

  navigate({
    pathname: "/",
    search: searchParams.toString(),
  });
}

export function NavigateToCategory(
  navigate: NavigateFunction,
  context: UCContext,
  category: ECategory
) {
  const searchParams = new URLSearchParamsEx(context);
  searchParams.SetCategory(category);
  NavigateTo(navigate, context, searchParams);
}

export function NavigateToRoot(navigate: NavigateFunction) {
  navigate("/");
}
