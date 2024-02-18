import { CommonUCService } from "./CommonUCService";
import { CurrencyUCService } from "./CurrencyUCService";
import { LinkZipUCService } from "./LinkZipUCService";
import { RadixUCService } from "./RadixUCService";
import { TemperatureUCService } from "./TemperatureUCService";

export class UCService implements IService {
  radix = new RadixUCService();
  currency = new CurrencyUCService();
  temperature = new TemperatureUCService();
  linkZip = new LinkZipUCService();
  common = new CommonUCService();
}
