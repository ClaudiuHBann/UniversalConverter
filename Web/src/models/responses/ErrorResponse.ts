class ErrorResponse extends BaseResponse {
  code: number = 0;
  message: string = "";

  constructor(code: number, message: string) {
    super();
    this.code = code;
    this.message = message;
  }
}
