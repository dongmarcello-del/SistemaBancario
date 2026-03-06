export type ResponseMessage<T> = {
    success: boolean; 
    message: string;
    data: T
}