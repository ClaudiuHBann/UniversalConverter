import { AppShell } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import Header from "./Header";
import Footer from "./Footer";
import HeaderItems from "./HeaderItems";

export interface AppShellExProps {
  children: React.ReactNode;
}

function AppShellEx({ children }: AppShellExProps) {
  const [opened, { toggle }] = useDisclosure();

  return (
    <AppShell
      header={{ height: 50 }}
      navbar={{
        width: 300,
        breakpoint: "sm",
        collapsed: { desktop: true, mobile: !opened },
      }}
      padding="md"
    >
      <AppShell.Header>
        <Header opened={opened} toggle={toggle} />
      </AppShell.Header>

      <AppShell.Navbar>
        <HeaderItems />
      </AppShell.Navbar>

      <AppShell.Footer>
        <Footer />
      </AppShell.Footer>

      <AppShell.Main>{children}</AppShell.Main>
    </AppShell>
  );
}

export default AppShellEx;
