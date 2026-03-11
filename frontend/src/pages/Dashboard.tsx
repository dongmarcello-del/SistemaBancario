import { useEffect, useState } from "react";
import Form from "../components/Form";
import { deposit, getCurrentBalance, getLast20Transaction, withdraw } from "../services/accountService";
import { useParams } from "react-router";
import TransactionTable from "../components/TransactionTable";
import type { transactionDto } from "../DTOs/account/transactionDto";
import { Box, Typography, Paper } from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    mode: "dark",
    primary: { main: "#00bcd4" },
    background: { default: "#0a0e1a", paper: "#111827" },
  },
  typography: { fontFamily: "'DM Mono', monospace" },
  shape: { borderRadius: 12 },
});

export default function Dashboard() {
  const [balance, setBalance] = useState(0);
  const [transactions, setTransactions] = useState<transactionDto[]>([]);
  const params = useParams();
  const accountId = params.idAccount;

  async function loadData() {
    const balanceRes = await getCurrentBalance(accountId);
    setBalance(balanceRes.data);
    const txRes = await getLast20Transaction(accountId);
    setTransactions(txRes.data);
  }

  useEffect(() => {
    loadData();
  }, [accountId]);

  return (
    <ThemeProvider theme={theme}>
      <Box
        sx={{
          minHeight: "100vh",
          background: "radial-gradient(ellipse at 50% 0%, #0d2137 0%, #0a0e1a 70%)",
          p: 4,
        }}
      >
        {/* Titolo */}
        <Typography
          variant="h4"
          sx={{
            fontFamily: "'DM Mono', monospace",
            fontWeight: 700,
            color: "#00bcd4",
            textTransform: "uppercase",
            letterSpacing: "0.1em",
            mb: 4,
          }}
        >
          Dashboard
        </Typography>

        {/* Balance card */}
        <Paper
          elevation={0}
          sx={{
            p: 4,
            mb: 4,
            border: "1px solid rgba(0,188,212,0.15)",
            background: "rgba(17,24,39,0.9)",
            backdropFilter: "blur(12px)",
            boxShadow: "0 0 60px rgba(0,188,212,0.06)",
            borderRadius: 3,
            display: "flex",
            alignItems: "center",
            gap: 3,
          }}
        >
          <Box
            sx={{
              p: 1.5,
              borderRadius: 2,
              background: "rgba(0,188,212,0.1)",
              border: "1px solid rgba(0,188,212,0.2)",
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              fontSize: "2rem",
            }}
          >
            💰
          </Box>
          <Box>
            <Typography sx={{ color: "#4a5568", fontSize: "0.75rem", textTransform: "uppercase", letterSpacing: "0.08em" }}>
              Saldo disponibile
            </Typography>
            <Typography sx={{ color: "#e2e8f0", fontSize: "2rem", fontWeight: 700, letterSpacing: "0.02em" }}>
              € {balance.toLocaleString("it-IT", { minimumFractionDigits: 2 })}
            </Typography>
          </Box>
        </Paper>

        {/* Forms affiancati */}
        <Box
          sx={{
            display: "grid",
            gridTemplateColumns: { xs: "1fr", md: "1fr 1fr" },
            gap: 3,
            mb: 4,
          }}
        >
          <Form
            title="Deposita soldi"
            fields={[{ name: "amount", type: "number", placeholder: "Quantità" }]}
            onSubmit={async (values) => {
              const res = await deposit({ accountId, ...values });
              await loadData();
              return res;
            }}
            dataHandler={undefined}
            submitText="Deposita"
          />
          <Form
            title="Preleva soldi"
            fields={[{ name: "amount", type: "number", placeholder: "Quantità" }]}
            onSubmit={async (values) => {
              const res = await withdraw({ accountId, ...values });
              await loadData();
              return res;
            }}
            dataHandler={undefined}
            submitText="Preleva"
          />
        </Box>

        {/* Tabella transazioni */}
        <Typography
          sx={{
            color: "#00bcd4",
            fontSize: "0.75rem",
            textTransform: "uppercase",
            letterSpacing: "0.08em",
            fontWeight: 700,
            mb: 2,
          }}
        >
          Ultime transazioni
        </Typography>
        <TransactionTable transactions={transactions} />
      </Box>
    </ThemeProvider>
  );
}