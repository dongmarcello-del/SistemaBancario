import { useState } from "react";
import type { FormProps } from "../props/FormProps";
import {
  Box,
  Button,
  TextField,
  Typography,
  Paper,
  Alert,
  Fade,
  CircularProgress,
} from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    mode: "dark",
    primary: { main: "#00bcd4" },
    background: { default: "#0a0e1a", paper: "#111827" },
  },
  typography: {
    fontFamily: "'DM Mono', monospace",
  },
  shape: { borderRadius: 12 },
});

export default function Form({ fields, onSubmit, dataHandler, submitText, title }: FormProps) {
  const [values, setValues] = useState(
    fields.reduce((acc, f) => ({ ...acc, [f.name]: "" }), {} as Record<string, string>)
  );
  const [responseOutput, setResponseOutput] = useState("");
  const [isError, setIsError] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);
    setResponseOutput("");

    const result = await onSubmit(values);

    if (result.data && dataHandler) dataHandler(result.data);

    setIsError(!result.success);
    setResponseOutput(result.message);
    setLoading(false);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setValues({ ...values, [e.target.name]: e.target.value });
  };

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
            maxWidth: 420,
            border: "1px solid rgba(0,188,212,0.15)",
            background: "rgba(17,24,39,0.9)",
            backdropFilter: "blur(12px)",
            boxShadow: "0 0 60px rgba(0,188,212,0.06)",
          }}
        >
          {/* Titolo opzionale */}
          {title && (
            <Typography
              variant="h5"
              sx={{
                mb: 3,
                fontFamily: "'DM Mono', monospace",
                fontWeight: 700,
                letterSpacing: "0.05em",
                color: "#00bcd4",
                textAlign: "center",
                textTransform: "uppercase",
                fontSize: "1.1rem",
              }}
            >
              {title}
            </Typography>
          )}

          <Box component="form" onSubmit={handleSubmit} sx={{ display: "flex", flexDirection: "column", gap: 2.5 }}>
            {fields.map((f) => (
              <TextField
                key={f.name}
                name={f.name}
                type={f.type || "text"}
                placeholder={f.placeholder || ""}
                value={values[f.name]}
                onChange={handleChange}
                variant="outlined"
                fullWidth
                sx={{
                  "& .MuiOutlinedInput-root": {
                    color: "#e2e8f0",
                    "& fieldset": { borderColor: "rgba(0,188,212,0.2)" },
                    "&:hover fieldset": { borderColor: "rgba(0,188,212,0.5)" },
                    "&.Mui-focused fieldset": { borderColor: "#00bcd4" },
                  },
                  "& input::placeholder": { color: "#4a5568" },
                }}
              />
            ))}

            <Button
              type="submit"
              variant="contained"
              disabled={loading}
              fullWidth
              sx={{
                mt: 1,
                py: 1.4,
                fontFamily: "'DM Mono', monospace",
                fontWeight: 600,
                letterSpacing: "0.08em",
                fontSize: "0.85rem",
                textTransform: "uppercase",
                background: "linear-gradient(135deg, #00bcd4, #0097a7)",
                boxShadow: "0 0 24px rgba(0,188,212,0.25)",
                "&:hover": {
                  boxShadow: "0 0 36px rgba(0,188,212,0.4)",
                  background: "linear-gradient(135deg, #26c6da, #00bcd4)",
                },
              }}
            >
              {loading ? <CircularProgress size={20} sx={{ color: "#fff" }} /> : submitText}
            </Button>

            <Fade in={!!responseOutput}>
              <Box>
                {responseOutput && (
                  <Alert
                    severity={isError ? "error" : "success"}
                    sx={{
                      fontFamily: "'DM Mono', monospace",
                      fontSize: "0.8rem",
                      background: isError ? "rgba(239,68,68,0.1)" : "rgba(0,188,212,0.1)",
                      border: `1px solid ${isError ? "rgba(239,68,68,0.3)" : "rgba(0,188,212,0.3)"}`,
                      color: isError ? "#fc8181" : "#81e6d9",
                      "& .MuiAlert-icon": { color: isError ? "#fc8181" : "#00bcd4" },
                    }}
                  >
                    {responseOutput}
                  </Alert>
                )}
              </Box>
            </Fade>
          </Box>
        </Paper>
      </Box>
    </ThemeProvider>
  );
}