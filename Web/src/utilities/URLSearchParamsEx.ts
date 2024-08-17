import { UCContext } from "../contexts/UCContext";
import { ECategory, ESearchParam } from "./Enums";
import { ToSearchParam } from "./EnumsExtensions";

export class URLSearchParamsEx {
  private context: UCContext | null;
  private urlSearchParams: URLSearchParams;

  public constructor(
    context: UCContext | null = null,
    search?: string,
    urlSearchParams?: URLSearchParams
  ) {
    this.context = context;
    this.urlSearchParams =
      urlSearchParams ?? new URLSearchParams(search ?? location.search);
  }

  public toString(): string {
    return this.urlSearchParams.toString();
  }

  private Get(param: ESearchParam): string | null {
    return this.urlSearchParams.get(param);
  }

  public GetCode(): string | null {
    return this.Get(ESearchParam.Code);
  }

  public GetCategory(): string | null {
    return this.Get(ESearchParam.Category);
  }

  public GetTo(): string | null {
    return this.Get(ESearchParam.To);
  }

  public GetFrom(): string | null {
    return this.Get(ESearchParam.From);
  }

  public GetFromOrTo(from: boolean): string | null {
    return from ? this.GetFrom() : this.GetTo();
  }

  private static IsFromTo(param: ESearchParam): boolean {
    return param === ESearchParam.From || param === ESearchParam.To;
  }

  private SetRaw(param: ESearchParam, value: string): boolean {
    this.urlSearchParams.set(param, value);
    return this.urlSearchParams.get(param) === value;
  }

  private SetFromTo(param: ESearchParam, value: string): boolean {
    if (!this.context || !URLSearchParamsEx.IsFromTo(param)) {
      return false;
    }

    const category = this.GetCategory();
    if (
      !this.context.HasCategory(category) ||
      !this.context.HasFromTo(category, value)
    ) {
      return false;
    }

    return this.SetRaw(param, value);
  }

  private Set(param: ESearchParam, value: string): boolean {
    // if we dont have a context we cant have additional checks
    // and the additional checks are only for from and to params
    if (!this.context || !URLSearchParamsEx.IsFromTo(param)) {
      return this.SetRaw(param, value);
    }

    return this.SetFromTo(param, value);
  }

  public SetFrom(value: string): boolean {
    return this.Set(ESearchParam.From, value);
  }

  public SetTo(value: string): boolean {
    return this.Set(ESearchParam.To, value);
  }

  public SetCategory(value: string): boolean {
    return this.Set(ESearchParam.Category, value);
  }

  public Map(): Map<ESearchParam, string> {
    const searchParamToValue = new Map<ESearchParam, string>();
    this.urlSearchParams.forEach((value, key) => {
      const searchParam = ToSearchParam(key);
      if (!searchParam) {
        return;
      }

      searchParamToValue.set(searchParam, value);
    });

    return searchParamToValue;
  }

  public AddCategoryAndFromOrTo(
    category: ECategory,
    searchParam: ESearchParam,
    value: string
  ): URLSearchParamsEx {
    if (!this.context) {
      return this;
    }

    if (!category || !this.context.HasCategory(category)) {
      return this;
    }

    this.SetCategory(this.context.FindCategory(category)!);

    if (!value || !this.context.HasFromTo(category, value)) {
      return this;
    }

    const fromTo = this.context.FindFromTo(category, value);
    if (!fromTo) {
      return this;
    }

    switch (searchParam) {
      case ESearchParam.From:
        this.SetFrom(fromTo);
        break;

      case ESearchParam.To:
        this.SetTo(fromTo);
        break;
    }

    return this;
  }
}
