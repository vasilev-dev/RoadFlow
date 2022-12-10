export default class ErrorHandler {
  public static getMessageFromError(error: unknown): string {
    // eslint-disable-next-line @typescript-eslint/ban-ts-comment
    // @ts-ignore
    return error.response?.data?.errorMessage ?? 'Something went wrong';
  }
}
