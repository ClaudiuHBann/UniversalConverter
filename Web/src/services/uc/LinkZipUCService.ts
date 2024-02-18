import { Axios } from "axios";
import { BaseUCService } from "./BaseUCService";

export class LinkZipUCService extends BaseUCService<
  LinkZipRequest,
  LinkZipResponse
> {
  GetControllerName() {
    return "LinkZip";
  }

  constructor(axios: Axios) {
    super(axios);
  }
}
