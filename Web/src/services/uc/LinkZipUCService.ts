import { BaseUCService } from "./BaseUCService";

export class LinkZipUCService extends BaseUCService<
  LinkZipRequest,
  LinkZipResponse
> {
  protected override GetControllerName() {
    return "LinkZip";
  }
}
