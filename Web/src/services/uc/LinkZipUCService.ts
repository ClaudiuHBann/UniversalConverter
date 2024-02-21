import { LinkZipRequest } from "../../models/requests/LinkZipRequest";
import { LinkZipResponse } from "../../models/responses/LinkZipResponse";
import { BaseUCService } from "./BaseUCService";

export class LinkZipUCService extends BaseUCService<
  LinkZipRequest,
  LinkZipResponse
> {
  protected override GetControllerName() {
    return "LinkZip";
  }
}
