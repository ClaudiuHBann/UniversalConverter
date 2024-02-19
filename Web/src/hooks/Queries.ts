import { useQuery, useMutation } from "@tanstack/react-query";
import { UCService } from "../services/uc/UCService";
import { BaseUCService } from "../services/uc/BaseUCService";
import { ECategory } from "../utilities/Enums";
import { BaseResponse } from "../models/responses/BaseResponse";
import { BaseRequest } from "../models/requests/BaseRequest";

const uc = new UCService();

const categoryToService = new Map<
  string,
  BaseUCService<BaseRequest, BaseResponse>
>([
  [ECategory.Currency, uc.currency],
  [ECategory.LinkZip, uc.linkZip],
  [ECategory.Radix, uc.radix],
  [ECategory.Temperature, uc.temperature],
]);

export const useFromToAll = () => {
  return useQuery({
    queryKey: ["fromToAll"],
    queryFn: async () => await uc.common.FromToAll(),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useFromTo = (category: ECategory) => {
  return useQuery({
    queryKey: ["fromTo", category],
    queryFn: async () => await categoryToService.get(category)?.FromTo(),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useConvert = (category: ECategory, request: BaseRequest) => {
  return useMutation({
    mutationKey: ["convert", category, request],
    mutationFn: async () =>
      await categoryToService.get(category)?.Convert(request),
  });
};
