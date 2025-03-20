<script>
  import MainLayout from "$lib/layouts/MainLayout.svelte";

  import { get } from "svelte/store";
  import { authStore } from "$lib/stores/authStore";

  import "./../scss/styles.scss";
  import "./../../node_modules/@fortawesome/fontawesome-free/scss/fontawesome.scss";
  import { onMount } from "svelte";

  const isAuthenticated = get(authStore).isAuthenticated;

  onMount(() => {
    if (isAuthenticated) {
      document.body.classList.add("bg-primary");
      return () => {
        document.body.classList.remove("bg-primary");
      };
    }
    return () => { };
  });
</script>

{#if isAuthenticated}
  <MainLayout>
    <slot />
  </MainLayout>
{:else}
  <slot />
{/if}
