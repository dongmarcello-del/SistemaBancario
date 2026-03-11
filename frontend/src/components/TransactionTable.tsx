import type { transactionDto } from "../DTOs/account/transactionDto";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Typography,
  Box,
} from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    mode: "dark",
    primary: { main: "#00bcd4" },
  },
  typography: { fontFamily: "'DM Mono', monospace" },
  shape: { borderRadius: 12 },
});

type TransactionTableProps = {
  transactions: transactionDto[];
};

export default function TransactionsTable({ transactions }: TransactionTableProps) {
  return (
    <ThemeProvider theme={theme}>
      <TableContainer
        component={Paper}
        elevation={0}
        sx={{
          border: "1px solid rgba(0,188,212,0.15)",
          background: "rgba(17,24,39,0.9)",
          backdropFilter: "blur(12px)",
          boxShadow: "0 0 60px rgba(0,188,212,0.06)",
          borderRadius: 0,
        }}
      >
        {transactions.length === 0 ? (
          <Box sx={{ py: 6, textAlign: "center" }}>
            <Typography sx={{ color: "#4a5568", fontSize: "0.85rem" }}>
              Nessuna transazione trovata
            </Typography>
          </Box>
        ) : (
          <Table>
            <TableHead>
              <TableRow>
                {["Data", "Quantità", "Tipo", "Mandante", "Ricevitore"].map((h) => (
                  <TableCell
                    key={h}
                    sx={{
                      color: "#00bcd4",
                      fontWeight: 700,
                      fontSize: "0.75rem",
                      textTransform: "uppercase",
                      letterSpacing: "0.08em",
                      borderBottom: "1px solid rgba(0,188,212,0.15)",
                    }}
                  >
                    {h}
                  </TableCell>
                ))}
              </TableRow>
            </TableHead>

            <TableBody>
              {transactions.map((t) => (
                <TableRow
                  key={t.id}
                  sx={{
                    "&:hover": { background: "rgba(0,188,212,0.04)" },
                    "& td": { borderBottom: "1px solid rgba(255,255,255,0.04)" },
                    transition: "background 0.2s",
                  }}
                >
                  <TableCell sx={{ color: "#a0aec0", fontSize: "0.82rem" }}>
                    {new Date(t.date).toLocaleString()}
                  </TableCell>
                  <TableCell sx={{ color: "#e2e8f0", fontWeight: 700, fontSize: "0.9rem" }}>
                    € {t.amount.toLocaleString("it-IT", { minimumFractionDigits: 2 })}
                  </TableCell>
                  <TableCell sx={{ color: "#e2e8f0", fontSize: "0.82rem" }}>
                    {t.cashOperationTypeString}
                  </TableCell>
                  <TableCell sx={{ color: "#718096", fontSize: "0.82rem" }}>
                    {t.senderAccountId ?? "—"}
                  </TableCell>
                  <TableCell sx={{ color: "#718096", fontSize: "0.82rem" }}>
                    {t.receiverAccountId ?? "—"}
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        )}
      </TableContainer>
    </ThemeProvider>
  );
}