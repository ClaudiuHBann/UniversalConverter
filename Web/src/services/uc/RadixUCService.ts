import { Axios } from "axios";
import { BaseUCService } from "./BaseUCService";

export class RadixUCService extends BaseUCService<RadixRequest, RadixResponse> {
  GetControllerName() {
    return "Radix";
  }

  constructor(axios: Axios) {
    super(axios);
  }
}
