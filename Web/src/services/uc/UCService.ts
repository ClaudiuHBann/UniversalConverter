import { Axios } from "axios";
import { CommonUCService } from "./CommonUCService";
import { CurrencyUCService } from "./CurrencyUCService";
import { LinkZipUCService } from "./LinkZipUCService";
import { RadixUCService } from "./RadixUCService";
import { TemperatureUCService } from "./TemperatureUCService";

export class UCService implements IService {
  radix: RadixUCService;
  currency: CurrencyUCService;
  temperature: TemperatureUCService;
  linkZip: LinkZipUCService;
  common: CommonUCService;

  constructor(axios: Axios) {
    this.radix = new RadixUCService(axios);
    this.currency = new CurrencyUCService(axios);
    this.temperature = new TemperatureUCService(axios);
    this.linkZip = new LinkZipUCService(axios);
    this.common = new CommonUCService(axios);
  }
}
