import Header from "@/components/header";
import Image from "next/image";
import mockup from "@/assets/mockup.webp";
import ScrollDown from "@/components/scroll-down";
import { Button } from "@/components/ui/button";
import { ChevronRight } from "lucide-react";
import { Suspense, use } from "react";
import { getAllSubscriptions } from "@/services/subscriptions-services";
import Loader from "@/components/loader";
import type Perk from "@/models/Perk";
import { getAllPerks } from "@/services/perks-services";

const CheckMarkIcon = () => (
  <svg
    stroke="currentColor"
    fill="currentColor"
    strokeWidth="0"
    viewBox="0 0 448 512"
    className="text-md text-green-300"
    height="1em"
    width="1em"
    xmlns="http://www.w3.org/2000/svg"
  >
    <path d="M438.6 105.4c12.5 12.5 12.5 32.8 0 45.3l-256 256c-12.5 12.5-32.8 12.5-45.3 0l-128-128c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0L160 338.7 393.4 105.4c12.5-12.5 32.8-12.5 45.3 0z"></path>
  </svg>
);

const CrossMarkIcon = () => (
  <svg
    stroke="currentColor"
    fill="currentColor"
    strokeWidth="0"
    viewBox="0 0 352 512"
    className="text-md text-red-300"
    height="1em"
    width="1em"
    xmlns="http://www.w3.org/2000/svg"
  >
    <path d="M242.72 256l100.07-100.07c12.28-12.28 12.28-32.19 0-44.48l-22.24-22.24c-12.28-12.28-32.19-12.28-44.48 0L176 189.28 75.93 89.21c-12.28-12.28-32.19-12.28-44.48 0L9.21 111.45c-12.28 12.28-12.28 32.19 0 44.48L109.28 256 9.21 356.07c-12.28 12.28-12.28 32.19 0 44.48l22.24 22.24c12.28 12.28 32.2 12.28 44.48 0L176 322.72l100.07 100.07c12.28 12.28 32.2 12.28 44.48 0l22.24-22.24c12.28-12.28 12.28-32.19 0-44.48L242.72 256z"></path>
  </svg>
);

const formatPerkText = (perk: Perk, value: number | null): string => {
  const name = perk.name.toLowerCase();

  if (name.includes("account")) {
    return `${value} ${value === 1 ? "account" : "accounts"}`;
  }
  if (name.includes("history") || name.includes("forecast")) {
    return `${value} days ${name}`;
  }
  return perk.name; // Para "Customizable", "Mobile App", etc.
};

export default function Home() {
  const subscriptions = use(getAllSubscriptions());
  const perks = use(getAllPerks());

  return (
    <main className="w-full bg-background">
      <Header />
      <div className="flex items-center justify-evenly pt-20 pl-14 h-dvh relative">
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
        <h1 className="text-7xl font-extrabold w-1/2 pl-20">
          A new way to manage your bills
        </h1>
        <ScrollDown />
      </div>
      <div className="flex items-center justify-between px-20 h-dvh relative">
        <h2 className="text-7xl font-bold w-1/2">
          Have as much control as possible over your bills
        </h2>
        <div className="w-2/5 space-y-4">
          <p className="font-light">
            With the <i>Eloquent™</i> you have control over your individual and
            shared expenses with accurate forecasts.
          </p>
          <Button variant="outline" className="w-36 hover:**:translate-x-1">
            See prices
            <ChevronRight className="transition-all" />
          </Button>
        </div>
        <ScrollDown />
      </div>
      <div className="xl:h-dvh min-h-dvh w-full xl:px-20 px-10 py-20 flex items-center justify-center gap-12 snap-end relative flex-col">
        <h1 className="font-bold text-4xl">
          Select the best offer for your use.
        </h1>

        {/* Grid para os cartões */}
        <div className="h-full w-full flex items-center justify-center xl:flex-row flex-col gap-12">
          <Suspense fallback={<Loader />}>
            {subscriptions.map((subscription) => (
              <div
                key={subscription.id}
                className="w-1/4 h-full flex flex-col items-center justify-start py-6 px-2 rounded-lg border relative overflow-clip box-border gap-4 hover:scale-105 transition-all cursor-pointer"
              >
                {/* Faixa decorativa (condicional para o plano 'Shared') */}
                {subscription.name === "Shared" && (
                  <div className="h-6 w-full -rotate-45 absolute -left-20 top-0 bg-foreground"></div>
                )}

                {/* Nome do Plano */}
                <h1 className="font-bold text-3xl">{subscription.name}</h1>

                {/* Preço */}
                <span className="flex items-start gap-1">
                  <h1 className="font-bold text-3xl">R${subscription.price}</h1>
                  <p>/month</p>
                </span>

                {/* Lista de Benefícios */}
                <ul className="dark:font-thin font-normal space-y-2 pb-12 pt-4">
                  {perks.map((perk) => {
                    // Verifica se o plano ATUAL possui este perk da lista mestra
                    const currentPerk = subscription.subscriptionPerks.find(
                      (sp) => sp.perkId === perk.id
                    );

                    return (
                      <li key={perk.id} className="flex items-center gap-2">
                        {currentPerk ? <CheckMarkIcon /> : <CrossMarkIcon />}

                        {currentPerk
                          ? formatPerkText(perk, currentPerk.value!)
                          : perk.name}
                      </li>
                    );
                  })}
                </ul>

                {/* Botão de Ação */}
                <Button variant={"outline"}>Start now</Button>
              </div>
            ))}
          </Suspense>
        </div>
      </div>
    </main>
  );
}
