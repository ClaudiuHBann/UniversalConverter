import { BaseUCService } from "./BaseUCService";

export class RadixUCService extends BaseUCService<RadixRequest, RadixResponse> {
  protected override GetControllerName() {
    return "Radix";
  }
}
