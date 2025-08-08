import Header from "@/components/header";
import Image from "next/image";
import mockup from "@/assets/mockup.webp";
import ScrollDown from "@/components/scroll-down";

export default function Home() {
  return (
    <main className="w-full min-h-dvh bg-background relative">
      <Header />
      <div className="flex items-center justify-evenly pt-20 pl-14 h-[90dvh]">
        <Image
          src={mockup}
          alt="Main Image"
          quality={80}
          loading="lazy"
          placeholder="blur"
          width={1000}
          height={1000}
          className="h-[70dvh] w-auto"
        />
        <h1 className="text-7xl font-extrabold w-1/2 pl-20">A new way to manage your bills</h1>
      </div>
        <ScrollDown />
    </main>
  );
}
  