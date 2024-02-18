import { Axios } from "axios";
import { BaseUCService } from "./BaseUCService";

export class TemperatureUCService extends BaseUCService<
  TemperatureRequest,
  TemperatureResponse
> {
  GetControllerName() {
    return "Temperature";
  }

  constructor(axios: Axios) {
    super(axios);
  }
}
