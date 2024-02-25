import { TemperatureRequest } from "../../models/requests/TemperatureRequest";
import { TemperatureResponse } from "../../models/responses/TemperatureResponse";
import { BaseUCService } from "./BaseUCService";

export class TemperatureUCService extends BaseUCService<
  TemperatureRequest,
  TemperatureResponse
> {
  public constructor() {
    super();
  }

  protected override GetControllerName() {
    return "Temperature";
  }
}
