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
import { useState, useEffect, useCallback } from "react";
import { ButtonLoading } from "@/components/button-loading";
import { ScrollArea } from "@/components/ui/scroll-area";
import Loader from "@/components/loader";
import { Checkbox } from "@/components/ui/checkbox";
import type Perk from "@/models/Perk";
import type Subscription from "@/models/Subscription";

const formSchema = z.object({
  name: z.string().min(2, "Name must be at least 2 characters.").max(50),
  description: z.string().min(5, "Description must be at least 5 characters."),
  price: z.preprocess(
    (val) => (val === "" ? null : parseFloat(String(val))),
    z.number().min(0, "Price must be a positive number.")
  ),
  subscriptionPerks: z
    .array(
      z.object({
        perkId: z.number(),
        value: z.number().min(0, "Value cannot be empty."),
      })
    )
    .optional(),
});

const API_URL = "http://localhost:8080/api";

export default function SubscriptionsPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [subscriptions, setSubscriptions] = useState<Subscription[]>([]);
  const [perks, setPerks] = useState<Perk[]>([]);
  const [selectedSubscription, setSelectedSubscription] =
    useState<Subscription | null>(null);

  const form = useForm({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: "",
      description: "",
      price: 0,
      subscriptionPerks: [],
    },
  });

  const fetchSubscriptions = async () => {
    const response = await fetch(`${API_URL}/Subscription`);
    if (!response.ok) throw new Error("Failed to fetch subscriptions");
    const data: Subscription[] = await response.json();
    setSubscriptions(data);
  };

  const fetchPerks = async () => {
    const response = await fetch(`${API_URL}/Perk`);
    if (!response.ok) throw new Error("Failed to fetch perks");
    const data: Perk[] = await response.json();
    setPerks(data);
  };

  useEffect(() => {
    const loadData = async () => {
      setIsLoading(true);
      try {
        await Promise.all([fetchSubscriptions(), fetchPerks()]);
      } catch (error) {
        console.error("Failed to load initial data", error);
        toast.error("Could not load page data.");
      } finally {
        setIsLoading(false);
      }
    };
    loadData();
  }, []);

  const handleClearForm = useCallback(() => {
    form.reset({
      name: "",
      description: "",
      price: 0,
      subscriptionPerks: [],
    });
    setSelectedSubscription(null);
  }, [form]);

  async function onSubmit(values: z.infer<typeof formSchema>) {
    setIsSubmitting(true);
    const isUpdating = selectedSubscription !== null;
    const url = isUpdating
      ? `${API_URL}/Subscription/${selectedSubscription.id}`
      : `${API_URL}/Subscription`;
    const method = isUpdating ? "PUT" : "POST";

    const payload = {
      ...values,
      ...(isUpdating && { id: selectedSubscription.id }),
      subscriptionPerks: (values.subscriptionPerks || []).map((p) => {
        return {
          perkId: p.perkId,
          value: p.value || 1,
        };
      }),
    };

    try {
      const response = await fetch(url, {
        method: method,
        body: JSON.stringify(payload),
        headers: { "Content-Type": "application/json" },
      });

      if (!response.ok) throw new Error("Server responded with an error");

      toast.success(
        `Subscription ${isUpdating ? "updated" : "created"} successfully!`
      );
      handleClearForm();
      await fetchSubscriptions();
    } catch (error) {
      toast.error(
        `Failed to ${isUpdating ? "update" : "create"} subscription.`
      );
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
  }

  const handleDeleteSubscription = async () => {
    if (!selectedSubscription) return;
    if (
      !window.confirm(
        `Are you sure you want to delete "${selectedSubscription.name}"?`
      )
    )
      return;

    setIsSubmitting(true);
    try {
      const response = await fetch(
        `${API_URL}/Subscription/${selectedSubscription.id}`,
        { method: "DELETE" }
      );
      if (!response.ok) throw new Error("Failed to delete subscription");
      toast.success("Subscription deleted successfully!");
      handleClearForm();
      await fetchSubscriptions();
    } catch (error) {
      toast.error("Failed to delete subscription.");
      console.error(error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleSubscriptionClick = useCallback(
    (subscription: Subscription) => {
      setSelectedSubscription(subscription);
      form.reset({
        name: subscription.name,
        description: subscription.description,
        price: subscription.price,
        subscriptionPerks: subscription.subscriptionPerks.map((sp) => ({
          perkId: sp.perkId,
          value: sp.value ?? 1,
        })),
      });
    },
    [form]
  );

  return (
    <main className="w-full h-dvh flex items-stretch justify-center gap-8 p-40">
      <Card className="flex-1">
        <CardHeader>
          <CardTitle>
            {selectedSubscription
              ? "Edit Subscription"
              : "Create a New Subscription"}
          </CardTitle>
          <CardDescription>
            {selectedSubscription
              ? `Editing "${selectedSubscription.name}"`
              : "Fill out the form to create a new subscription."}
          </CardDescription>
        </CardHeader>
        <CardContent>
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="name"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Subscription Name</FormLabel>
                    <FormControl>
                      <Input placeholder="E.g., Basic Plan" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Subscription Description</FormLabel>
                    <FormControl>
                      <Input
                        placeholder="E.g., Includes basic features"
                        {...field}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                control={form.control}
                name="price"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Monthly Price</FormLabel>
                    <FormControl>
                      <Input
                        type="number"
                        step="0.01"
                        placeholder="E.g., 9.99"
                        {...field}
                        value={field.value as string}
                      />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <div className="space-y-4">
                <FormLabel>Perks</FormLabel>
                <ScrollArea className="h-40 w-full">
                  <div className="space-y-2 rounded-md border p-4">
                    {perks.length > 0 ? (
                      perks.map((perk) => (
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
                                          { perkId: perk.id, value: "" },
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
                                <FormMessage />
                                {selectedPerk && (
                                  <Input
                                    placeholder="Value"
                                    type="number"
                                    className="h-8 w-40"
                                    value={Number(selectedPerk.value) || 1}
                                    onChange={(e) => {
                                      const updatedPerks = (
                                        field.value || []
                                      ).map((p) =>
                                        p.perkId === perk.id
                                          ? {
                                              ...p,
                                              value: Number(e.target.value),
                                            }
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
                      ))
                    ) : (
                      <p className="text-sm text-muted-foreground">
                        No perks available.
                      </p>
                    )}
                  </div>
                </ScrollArea>
              </div>

              <div className="flex items-center gap-2 flex-wrap pt-4">
                {isSubmitting ? (
                  <ButtonLoading />
                ) : (
                  <>
                    <Button type="submit" disabled={isSubmitting}>
                      {selectedSubscription ? "Update" : "Create"}
                    </Button>
                    {selectedSubscription && (
                      <Button
                        type="button"
                        variant="destructive"
                        onClick={handleDeleteSubscription}
                        disabled={isSubmitting}
                      >
                        Delete
                      </Button>
                    )}
                  </>
                )}
                <Button
                  type="button"
                  variant="secondary"
                  onClick={handleClearForm}
                >
                  Clear
                </Button>
              </div>
            </form>
          </Form>
        </CardContent>
      </Card>

      <Card className="flex-1">
        <CardHeader>
          <CardTitle>Available Subscriptions</CardTitle>
          <CardDescription>Click a subscription to edit it.</CardDescription>
        </CardHeader>
        <CardContent>
          <ScrollArea className="w-full h-96">
            {isLoading ? (
              <div className="w-full h-96 grid place-items-center">
                <Loader />
              </div>
            ) : subscriptions.length === 0 ? (
              <p>No subscriptions available.</p>
            ) : (
              <div className="space-y-2">
                {subscriptions.map((sub) => (
                  <Button
                    key={sub.id}
                    variant={
                      selectedSubscription?.id === sub.id
                        ? "default"
                        : "outline"
                    }
                    className="w-full h-auto text-left flex flex-col items-start p-4"
                    onClick={() => handleSubscriptionClick(sub)}
                  >
                    <h3 className="font-bold">{sub.name}</h3>
                    <p className="text-sm text-muted-foreground">
                      {sub.description}
                    </p>
                  </Button>
                ))}
              </div>
            )}
          </ScrollArea>
        </CardContent>
      </Card>
    </main>
  );
}
