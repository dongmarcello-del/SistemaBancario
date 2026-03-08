import { Navigate } from "react-router-dom"

type Props = {
  children: React.ReactNode
}

export function isAuthenticated() {
  return !!localStorage.getItem("token")
}

export default function ProtectedRoute({ children }: Props) {

  if (!isAuthenticated()) {
    return <Navigate to="/" replace />
  }

  return children
}