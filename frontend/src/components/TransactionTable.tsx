import type { transactionDto } from "../DTOs/account/transactionDto"

type TransactionTableProps = {
  transactions: transactionDto[]
}

export default function TransactionsTable({ transactions }: TransactionTableProps) {
  return (
    <table>
      <thead>
        <tr>
          <th>Data</th>
          <th>Quantità</th>
          <th>Tipo</th>
          <th>Mandante</th>
          <th>Ricevitore</th>
        </tr>
      </thead>

      <tbody>
        {transactions.map((t) => (
          <tr key={t.id}>
            <td>{new Date(t.date).toLocaleString()}</td>
            <td>{t.amount}</td>
            <td>{t.cashOperationTypeString}</td>
            <td>{t.senderAccountId ?? "-"}</td>
            <td>{t.receiverAccountId ?? "-"}</td>
          </tr>
        ))}
      </tbody>
    </table>
  )
}