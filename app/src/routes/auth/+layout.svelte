<script>
  import AuthLayout from "$lib/layouts/AuthLayout.svelte";
  import { onMount } from "svelte";

  onMount(() => {
    document.body.classList.add("bg-primary");
    return () => {
      document.body.classList.remove("bg-primary");
    };
  });
</script>

<AuthLayout>
    <slot />
</AuthLayout>