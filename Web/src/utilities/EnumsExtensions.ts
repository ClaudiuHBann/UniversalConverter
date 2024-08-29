import { EException } from "../models/exceptions/BaseException";
import { ERequest } from "../models/requests/BaseRequest";
import { EResponse } from "../models/responses/BaseResponse";
import { EHTTPRequest } from "../services/uc/BaseUCService";
import { FindIndex } from "./ArrayExtensions";
import { ECategory, ESearchParam } from "./Enums";

export function ToSearchParam(param: string): ESearchParam | null {
  const paramIndex = FindIndex(Object.keys(ESearchParam), param);
  return paramIndex === -1 ? null : Object.values(ESearchParam)[paramIndex];
}

export function ToCategory(category: string | null): ECategory | null {
  if (!category) {
    return null;
  }

  const categoryIndex = FindIndex(Object.keys(ECategory), category);
  return categoryIndex === -1 ? null : Object.values(ECategory)[categoryIndex];
}

export function ToStringEResponse(type: EResponse) {
  return Object.keys(EResponse)[type];
}

export function ToStringEException(type: EException) {
  return Object.keys(EException)[type];
}

export function ToStringEHTTPRequest(type: EHTTPRequest) {
  return Object.keys(EHTTPRequest)[type];
}

export function ToStringERequest(type: ERequest) {
  return Object.keys(ERequest)[type];
}
