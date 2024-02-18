import { BaseUCService } from "./BaseUCService";

export class TemperatureUCService extends BaseUCService<
  TemperatureRequest,
  TemperatureResponse
> {
  protected override GetControllerName() {
    return "Temperature";
  }
}
