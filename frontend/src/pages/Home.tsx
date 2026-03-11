import { useNavigate } from "react-router-dom";
import ComboBoxWithButton from "../components/Combobox";
import { useEffect, useState } from "react";
import { getAccountsId } from "../services/accountService";
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

export default function Home() {
  const navigate = useNavigate();
  const [accountsId, setAccountsId] = useState([]);

  async function loadData() {
    const accountsId = await getAccountsId();
    setAccountsId(accountsId.data);
  }

  useEffect(() => {
    loadData();
  }, []);

  return (
    <ThemeProvider theme={theme}>
      <Box
        sx={{
          minHeight: "100vh",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          background: "radial-gradient(ellipse at 50% 0%, #0d2137 0%, #0a0e1a 70%)",
        }}
      >
        <Paper
          elevation={0}
          sx={{
            p: 5,
            width: "100%",
            maxWidth: 440,
            border: "1px solid rgba(0,188,212,0.15)",
            background: "rgba(17,24,39,0.9)",
            backdropFilter: "blur(12px)",
            boxShadow: "0 0 60px rgba(0,188,212,0.06)",
            borderRadius: 3,
          }}
        >
          <Typography
            variant="h5"
            sx={{
              fontFamily: "'DM Mono', monospace",
              fontWeight: 700,
              color: "#00bcd4",
              textTransform: "uppercase",
              letterSpacing: "0.1em",
              mb: 1,
            }}
          >
            Sistema Bancario
          </Typography>

          <Typography
            sx={{
              color: "#4a5568",
              fontSize: "0.82rem",
              mb: 4,
            }}
          >
            Seleziona un account per accedere alla dashboard
          </Typography>

          <ComboBoxWithButton
            options={accountsId}
            label="Account"
            onSubmit={(value) => {
              navigate("/dashboard/" + value);
            }}
          />
        </Paper>
      </Box>
    </ThemeProvider>
  );
}