import { useQuery, useMutation } from "@tanstack/react-query";
import { UCService } from "../services/uc/UCService";
import { BaseUCService } from "../services/uc/BaseUCService";
import { Category } from "../utilities/Enums";

const uc = new UCService();

const categoryToService = new Map<
  string,
  BaseUCService<BaseRequest, BaseResponse>
>([
  [Category.Currency, uc.currency],
  [Category.LinkZip, uc.linkZip],
  [Category.Radix, uc.radix],
  [Category.Temperature, uc.temperature],
]);

export const useFromToAll = () => {
  return useQuery({
    queryKey: ["fromToAll"],
    queryFn: async () => await uc.common.FromToAll(),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useFromTo = (category: Category) => {
  return useQuery({
    queryKey: ["fromTo", category],
    queryFn: async () => await categoryToService.get(category)?.FromTo(),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useConvert = (category: Category, request: BaseRequest) => {
  return useMutation({
    mutationKey: ["convert", category],
    mutationFn: async () =>
      await categoryToService.get(category)?.Convert(request),
  });
};
