import Image from "next/image";
import React from "react";
import { Button } from "./ui/button";
import icon from "@/assets/icon.webp";
import { ModeToggle } from "./mode-toggle";

export default function Header() {
  return (
    <header className="flex items-center justify-between px-4 py-2 h-[10dvh] w-full backdrop-blur-lg fixed top-0 left-0 shadow-md">
      <Image src={icon} alt="Logo" width={100} height={100} className="h-full w-auto" />

      <div className="flex items-center space-x-4 justify-stretch">
        <Button variant={"outline"}>Sign In</Button>
        <Button variant={"outline"}>Sign Up</Button>
        <ModeToggle/>
      </div>
    </header>
  );
}
