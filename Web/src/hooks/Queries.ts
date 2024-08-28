import { useQuery, useMutation } from "@tanstack/react-query";
import { UCService } from "../services/uc/UCService";
import { BaseUCService } from "../services/uc/BaseUCService";
import { ECategory } from "../utilities/Enums";
import { BaseResponse } from "../models/responses/BaseResponse";
import { BaseRequest } from "../models/requests/BaseRequest";
import { RankRequest } from "../models/requests/RankRequest";
import { UCContext } from "../contexts/UCContext";
import { Notification } from "../components/Notification";

function CallbackPromiseCatchDefault(error: any) {
  Notification(error.message);
  return null;
}

function FindCallbackPromiseCatch(context?: UCContext) {
  return context
    ? context.GetCallbackPromiseCatch()
    : CallbackPromiseCatchDefault;
}

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

export const useFromToAll = (context: UCContext) => {
  return useQuery({
    queryKey: ["fromToAll"],
    queryFn: async () =>
      await uc.common.FromToAll().catch(context.GetCallbackPromiseCatch()),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useFromTo = (context: UCContext, category: ECategory) => {
  return useQuery({
    queryKey: ["fromTo", category],
    queryFn: async () =>
      await categoryToService
        .get(category)
        ?.FromTo()
        .catch(context.GetCallbackPromiseCatch()),
    gcTime: 1000 * 60 * 60 * 24, // a day
  });
};

export const useConvert = (category: ECategory, context?: UCContext) => {
  return useMutation({
    mutationFn: async (request: BaseRequest) =>
      await categoryToService
        .get(category)
        ?.Convert(request)
        .catch(FindCallbackPromiseCatch(context)),
  });
};

export const useRankConverters = (context: UCContext, request: RankRequest) => {
  return useQuery({
    queryKey: ["rankConverters", request],
    queryFn: async () =>
      await uc.rank
        .Converters(request)
        .catch(context.GetCallbackPromiseCatch()),
    gcTime: 1000 * 60, // a minute
  });
};
