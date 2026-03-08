export type transactionDto = {
  id: number
  date: string
  amount: number
  senderAccountId?: string
  receiverAccountId?: string
  cashOperationTypeString: string
}
