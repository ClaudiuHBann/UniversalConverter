class CommonResponse extends BaseResponse {
  fromToAll: Map<string, string[]> = new Map<string, string[]>();

  constructor(fromToAll: Map<string, string[]>) {
    super();
    this.fromToAll = fromToAll;
  }
}
