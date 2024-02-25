import { RadixRequest } from "../../models/requests/RadixRequest";
import { RadixResponse } from "../../models/responses/RadixResponse";
import { BaseUCService } from "./BaseUCService";

export class RadixUCService extends BaseUCService<RadixRequest, RadixResponse> {
  public constructor() {
    super();
  }

  protected override GetControllerName() {
    return "Radix";
  }
}
