import type { ResponseMessage } from "../DTOs/ResponseMessage";
import type { Field } from "./Field";

export type FormProps = {
    fields: Field[];
    onSubmit: (data: any) => Promise<ResponseMessage<any>>;
    dataHandler?: (data: any) => void | null;
    submitText?: string;
    title?: string;
}