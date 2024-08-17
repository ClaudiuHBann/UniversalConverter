import { useQuery, useMutation } from "@tanstack/react-query";
import { UCService } from "../services/uc/UCService";
import { BaseUCService } from "../services/uc/BaseUCService";
import { ECategory } from "../utilities/Enums";
import { BaseResponse } from "../models/responses/BaseResponse";
import { BaseRequest } from "../models/requests/BaseRequest";
import { NotificationEx } from "../components/Notification";
import { RankRequest } from "../models/requests/RankRequest";

const uc = new UCService();

function CallbackDefault(error: any) {
  NotificationEx(error.message);
  return null;
}

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
    queryFn: async () => await uc.common.FromToAll().catch(CallbackDefault),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useFromTo = (category: ECategory) => {
  return useQuery({
    queryKey: ["fromTo", category],
    queryFn: async () =>
      await categoryToService.get(category)?.FromTo().catch(CallbackDefault),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useConvert = (category: ECategory) => {
  return useMutation({
    mutationFn: async (request: BaseRequest) =>
      await categoryToService
        .get(category)
        ?.Convert(request)
        .catch(CallbackDefault),
  });
};

export const useRankConverters = (request: RankRequest) => {
  return useQuery({
    queryKey: ["rankConverters", request],
    queryFn: async () =>
      await uc.rank.Converters(request).catch(CallbackDefault),
    gcTime: 1000 * 60, // a minute
  });
};
