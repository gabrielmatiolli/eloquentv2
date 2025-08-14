import Header from "@/components/header";
import Image from "next/image";
import mockup from "@/assets/mockup.webp";
import ScrollDown from "@/components/scroll-down";
import { Button } from "@/components/ui/button";
import { ChevronRight } from "lucide-react";
import { dados } from "@/assets/data";
import {
  Card,
  CardAction,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

export default function Home() {
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
      <div className="flex flex-col items-center justify-between py-28 h-dvh relative w-full px-20">
        <h3 className="text-3xl font-bold">
          Select the best offer for your use.
        </h3>
        <div className="grid grid-cols-4 gap-8 w-full">
          {dados.map((plano) => (
            <Card key={plano.id} className="hover:scale-105 transition-all relative overflow-hidden">
              <CardHeader>
                <CardTitle className="text-center text-2xl font-bold">
                  {plano.nome}
                </CardTitle>
                <CardDescription className="text-center font-bold text-2xl text-foreground flex items-start justify-center gap-2">
                  {`R$ ${plano.price},00`}
                  <span className="text-sm font-light">/month</span>
                </CardDescription>
              </CardHeader>
              <CardContent className="flex-1">
                <div className="h-4 w-full bg-foreground absolute top-0 -left-20 -rotate-45"></div>
                <ul>
                  {Object.entries(plano.beneficios).map(([key, value]) => (
                    <li key={key}>
                      {key}: {value.toString()}
                    </li>
                  ))}
                </ul>
              </CardContent>
              <CardFooter className="w-full text-center">
                <CardAction className="mx-auto">
                  <Button variant={"secondary"} className="mx-auto">Start now</Button>
                </CardAction>
              </CardFooter>
            </Card>
          ))}
        </div>
        <ScrollDown />
      </div>
    </main>
  );
}
