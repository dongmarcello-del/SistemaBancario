import { useState } from "react";
import { Box, Typography, Select, MenuItem, Button } from "@mui/material";
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

type ComboBoxWithButtonProps = {
  options: string[];
  label?: string;
  onSubmit: (value: string) => void;
};

export default function ComboBoxWithButton({ options, label, onSubmit }: ComboBoxWithButtonProps) {
  const [selected, setSelected] = useState("");

  const handleClick = () => {
    if (selected) onSubmit(selected);
  };

  return (
    <ThemeProvider theme={theme}>
      <Box sx={{ display: "flex", flexDirection: "column", gap: 1.5 }}>
        {label && (
          <Typography sx={{ color: "#00bcd4", fontSize: "0.75rem", textTransform: "uppercase", letterSpacing: "0.08em", fontWeight: 700 }}>
            {label}
          </Typography>
        )}

        <Box sx={{ display: "flex", gap: 2 }}>
          <Select
            value={selected}
            onChange={(e) => setSelected(e.target.value)}
            displayEmpty
            fullWidth
            sx={{
              fontFamily: "'DM Mono', monospace",
              color: selected ? "#e2e8f0" : "#4a5568",
              "& .MuiOutlinedInput-notchedOutline": { borderColor: "rgba(0,188,212,0.2)" },
              "&:hover .MuiOutlinedInput-notchedOutline": { borderColor: "rgba(0,188,212,0.5)" },
              "&.Mui-focused .MuiOutlinedInput-notchedOutline": { borderColor: "#00bcd4" },
              "& .MuiSvgIcon-root": { color: "#00bcd4" },
              background: "rgba(17,24,39,0.9)",
            }}
          >
            <MenuItem value="" disabled sx={{ fontFamily: "'DM Mono', monospace", color: "#4a5568" }}>
              Seleziona...
            </MenuItem>
            {options.map((opt, idx) => (
              <MenuItem key={idx} value={opt} sx={{ fontFamily: "'DM Mono', monospace", color: "#e2e8f0" }}>
                {opt}
              </MenuItem>
            ))}
          </Select>

          <Button
            onClick={handleClick}
            disabled={!selected}
            variant="contained"
            sx={{
              fontFamily: "'DM Mono', monospace",
              fontWeight: 600,
              letterSpacing: "0.08em",
              fontSize: "0.85rem",
              textTransform: "uppercase",
              px: 3,
              background: "linear-gradient(135deg, #00bcd4, #0097a7)",
              boxShadow: "0 0 24px rgba(0,188,212,0.25)",
              "&:hover": {
                boxShadow: "0 0 36px rgba(0,188,212,0.4)",
                background: "linear-gradient(135deg, #26c6da, #00bcd4)",
              },
              "&.Mui-disabled": {
                background: "rgba(0,188,212,0.1)",
                color: "#4a5568",
              },
            }}
          >
            Vai
          </Button>
        </Box>
      </Box>
    </ThemeProvider>
  );
}