"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { toast } from "sonner";
import { useState, useEffect, use, Suspense } from "react";
import { ButtonLoading } from "@/components/button-loading";
import { ScrollArea } from "@/components/ui/scroll-area";
import Loader from "@/components/loader";
import { Checkbox } from "@/components/ui/checkbox";
import type Perk from "@/models/Perk";
import type Subscription from "@/models/Subscription";
import { ArrowLeft } from "lucide-react";
import Link from "next/link";
import type SubscriptionPerk from "@/models/SubscriptionPerk";

// Schema de validação focado apenas nos perks
const perksFormSchema = z.object({
  subscriptionPerks: z
    .array(
      z.object({
        perkId: z.number(),
        subscriptionId: z.number(),
        value: z.preprocess(
          (val) => (String(val).trim() === "" ? undefined : Number(val)),
          z
            .number({ message: "Value is required." })
            .min(1, { message: "Value must be at least 1." })
        ) as z.ZodType<number, number>,
      })
    )
    .optional(),
});

const API_URL = "http://localhost:8080/api";

// A página recebe 'params' com o ID da URL, ex: /subscriptions/1/perks
export default function UpdateSubscriptionPerksPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id: subscriptionId } = use(params);

  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [subscription, setSubscription] = useState<Subscription | null>(null);
  const [allPerks, setAllPerks] = useState<Perk[]>([]);

  type PerksFormType = z.infer<typeof perksFormSchema>;
  const form = useForm<PerksFormType>({
    resolver: zodResolver(perksFormSchema),
    defaultValues: {
      subscriptionPerks: [],
    },
  });

  useEffect(() => {
    const loadData = async () => {
      if (!subscriptionId) return;
      setIsLoading(true);
      try {
        const [subResponse, subPerksResponse, allPerksResponse] =
          await Promise.all([
            // 1. Busca os detalhes da assinatura (para o nome)
            fetch(`${API_URL}/Subscription/${subscriptionId}`),
            // 2. Usa sua nova rota para buscar os perks JÁ associados
            fetch(`${API_URL}/SubscriptionPerk/subscription/${subscriptionId}`),
            // 3. Busca TODOS os perks para montar a lista de checkboxes
            fetch(`${API_URL}/Perk`),
          ]);

        if (!subResponse.ok || !subPerksResponse.ok || !allPerksResponse.ok) {
          throw new Error("Failed to fetch initial data");
        }

        const subData: Subscription = await subResponse.json();
        const subPerksData: SubscriptionPerk[] = await subPerksResponse.json(); // Dados da sua nova rota
        const allPerksData: Perk[] = await allPerksResponse.json();

        setSubscription(subData);
        setAllPerks(allPerksData);

        // Popula o formulário com os dados da sua rota específica
        form.reset({
          subscriptionPerks: subPerksData.map((sp) => ({
            subscriptionId: Number(subscriptionId),
            perkId: sp.perkId,
            value: sp.value ?? 1, // Usa ?? 1 para perks que não exigem valor (como "Customizable")
          })),
        });
      } catch (error) {
        console.error("Failed to load data", error);
        toast.error("Could not load subscription data.");
      } finally {
        setIsLoading(false);
      }
    };
    loadData();
  }, [subscriptionId, form]);

  async function onSubmit(values: z.infer<typeof perksFormSchema>) {
    setIsSubmitting(true);
    // Endpoint específico para atualizar apenas os perks
    const url = `${API_URL}/SubscriptionPerk/subscription/${subscriptionId}`;

    try {
      const response = await fetch(url, {
        method: "PUT",
        // Envia apenas a lista de perks no corpo da requisição
        body: JSON.stringify(values.subscriptionPerks || []),
        headers: { "Content-Type": "application/json" },
      });

      if (!response.ok) {
        const errorData = await response.json();
        console.error("Server error:", errorData);
        throw new Error("Server responded with an error");
      }

      toast.success(`Perks for "${subscription?.name}" updated successfully!`);
    } catch (error) {
      toast.error(`Failed to update perks.`);
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
  }

  if (isLoading) {
    return (
      <div className="w-full h-dvh grid place-items-center">
        <Loader />
      </div>
    );
  }

  if (!subscription) {
    return (
      <div className="w-full h-dvh grid place-items-center">
        <p>Subscription not found.</p>
      </div>
    );
  }

  return (
    <Suspense fallback={<Loader />}>
      <main className="w-full min-h-dvh flex items-center justify-center p-4 sm:p-8 md:p-24">
        <Card className="w-full max-w-2xl">
          <CardHeader>
            <Link
              href="/subscriptions"
              className="flex items-center gap-2 text-sm text-muted-foreground hover:text-primary mb-4"
            >
              <ArrowLeft size={16} />
              Back to Subscriptions
            </Link>
            <CardTitle>Edit Perks for &quot;{subscription.name}&quot; </CardTitle>
            <CardDescription>
              Select the perks and define their values for this subscription
              plan.
            </CardDescription>
          </CardHeader>
          <CardContent>
            <Form {...form}>
              <form
                onSubmit={form.handleSubmit(onSubmit)}
                className="space-y-6"
              >
                <div className="space-y-4">
                  <FormLabel>Available Perks</FormLabel>
                  <ScrollArea className="h-64 w-full">
                    <div className="space-y-4 rounded-md border p-4">
                      {allPerks.map((perk) => (
                        <FormField
                          key={perk.id}
                          control={form.control}
                          name="subscriptionPerks"
                          render={({ field }) => {
                            const selectedPerk = field.value?.find(
                              (p) => p.perkId === perk.id
                            );
                            return (
                              <FormItem className="flex flex-row items-center space-x-3 space-y-0">
                                <FormControl>
                                  <Checkbox
                                    checked={!!selectedPerk}
                                    onCheckedChange={(checked: boolean) => {
                                      const currentPerks = field.value || [];
                                      if (checked) {
                                        field.onChange([
                                          ...currentPerks,
                                          {
                                            perkId: perk.id,
                                            value: 1,
                                            subscriptionId:
                                              Number(subscriptionId),
                                          },
                                        ]);
                                      } else {
                                        field.onChange(
                                          currentPerks.filter(
                                            (p) => p.perkId !== perk.id
                                          )
                                        );
                                      }
                                    }}
                                  />
                                </FormControl>
                                <FormLabel className="flex-1 font-normal">
                                  {perk.name}
                                </FormLabel>
                                {selectedPerk && (
                                  <FormMessage className="text-xs" />
                                )}
                                {selectedPerk && (
                                  <Input
                                    placeholder="Value"
                                    type="number"
                                    className="h-8 w-40"
                                    value={selectedPerk.value ?? ""}
                                    onChange={(e) => {
                                      const updatedPerks = (
                                        field.value || []
                                      ).map((p) =>
                                        p.perkId === perk.id
                                          ? { ...p, value: e.target.value }
                                          : p
                                      );
                                      field.onChange(updatedPerks);
                                    }}
                                  />
                                )}
                              </FormItem>
                            );
                          }}
                        />
                      ))}
                    </div>
                  </ScrollArea>
                </div>
                <div className="flex items-center gap-2 pt-4">
                  {isSubmitting ? (
                    <ButtonLoading />
                  ) : (
                    <Button type="submit" disabled={isSubmitting}>
                      Save Changes
                    </Button>
                  )}
                </div>
              </form>
            </Form>
          </CardContent>
        </Card>
      </main>
    </Suspense>
  );
}
